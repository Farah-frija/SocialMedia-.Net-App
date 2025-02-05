using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Core.Application.DTOs;
using Core.Domain.Entities;

using Core.Application.DTOs.DTOsRequests.MessageRequests;
using Core.Application.Interfaces.Chat;
using Core.Application.DTOs.DTOsResponses.MessageResponse;
using Core.Application.DTOs.DTOsResponses.ProfileResponse.UserAsIconDto;
using Microsoft.AspNetCore.Authorization;

[Route("api/messages")]
[ApiController]
[Authorize] // Requires authentication

public class MessageController : ControllerBase
{
    private readonly IMessageSerice _messageService;
    private readonly IMapper _mapper;

    public MessageController(IMessageSerice messageService, IMapper mapper)
    {
        _messageService = messageService;
        _mapper = mapper;
    }

    // Get all messages in a chat room
    [HttpGet("chatRoom/{chatRoomId}")]
    public async Task<IActionResult> GetChatRoomMessages(string chatRoomId)
    {
        if (string.IsNullOrEmpty(chatRoomId))
        {
            return BadRequest(new { Message = "ChatRoom ID cannot be empty" });
        }

        var messages = await _messageService.GetChatRoomMessagesAsync(chatRoomId);
        // var messageDtos = _mapper.Map<ListOfMessagseDto>(messages);
        var messageDtos = messages.Select(m => new ListOfMessagseDto
        {
            Id = m.Id,
            Sender = new UserAsIconInTheMessage
            {
                Id = m.SenderId,
                UserName = m.Sender.UserName // Assure-toi que `Sender` est chargé depuis la base de données
            },
            ChatRoomId = m.ChatRoomId,
            Content = m.Content,  // Decrypt here if needed
            CreatedAt = m.CreatedAt
        }).ToList();

        return Ok(new { Message = "Chat room messages retrieved successfully", Messages = messageDtos});
    }

    // Send a message in a chat room
    [HttpPost]

    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto sendMessageDto)
    {
        // Validation des entrées
        if (sendMessageDto == null || string.IsNullOrEmpty(sendMessageDto.Content) ||
            string.IsNullOrEmpty(sendMessageDto.ChatRoomId) || string.IsNullOrEmpty(sendMessageDto.SenderId))
        {
            return BadRequest(new { Message = "Invalid input data" });
        }

        try
        {
            // Création du message
            var message = new Message
            {
                ChatRoomId = sendMessageDto.ChatRoomId,
                Content = sendMessageDto.Content,
                SenderId = sendMessageDto.SenderId
            };

            // Envoi du message via le service
            var result = await _messageService.SendMessageAsync(message);

            // Vérification du résultat
            if (!result.Succeeded)
            {
                // Si l'envoi a échoué, retourne les erreurs
                return BadRequest(new { Message = "Failed to send message", Errors = result.Errors });
            }

            // Si l'envoi a réussi, retourne une réponse réussie
            var messageDto = new ListOfMessagseDto
            {
                ChatRoomId = message.ChatRoomId,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                Id = message.Id,
                Sender = new UserAsIconInTheMessage
                {
                    Id = message.SenderId,
                   
                }
            };

            return Ok(new { Message = "Message sent successfully", MessageDetails = messageDto });
        }
        catch (Exception ex)
        {
            // Gestion des erreurs générales
            return StatusCode(500, new { Message = "Failed to send message", Error = ex.InnerException });
        }
    }
}
