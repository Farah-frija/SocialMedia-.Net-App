using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Domain.Entities;

using Core.Application.Interfaces.Chat.Core.Domain.ServiceInterfaces.Chats;
using Core.Application.DTOs.DTOsRequests.ChatRoomRequests;
using Core.Application.DTOs.DTOsResponses.ChatRoomResponse;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requires authentication

    public class ChatRoomController(IChatRoomService chatRoomService, IMapper mapper) : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService = chatRoomService;
        private readonly IMapper _mapper = mapper;

        // Create a new chat room
        [HttpPost("Create")]
        public async Task<IActionResult> CreateChatRoom([FromBody] CreateChatRoomDto createChatRoomDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data", Errors = ModelState });
            }

            var chatroom = _mapper.Map<ChatRoom>(createChatRoomDto);


            var result = await _chatRoomService.CreateChatRoomAsync(chatroom,createChatRoomDto.MembersId);

            if (!result.Succeeded)
            {
            
                return BadRequest(new { Message = "Failed to create chat room",  result.Errors});
            }

            return Ok(new { Message = "Chat room created successfully" });
        }

        // Add a user to a chat room
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserToChatRoom([FromBody] AddUserToChatRoomDto addUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data", Errors = ModelState });
            }

            var result = await _chatRoomService.AddUserToChatRoomAsync(addUserDto.ChatRoomId, addUserDto.UserId);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Failed to add user to chat room", Errors = result.Errors });
            }

            return Ok(new { Message = "User added to chat room successfully" });
        }

        // Remove a user from a chat room
        [HttpPost("RemoveUser")]
        public async Task<IActionResult> RemoveUserFromChatRoom([FromBody] RemoveUserFromChatRoomDto removeUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid data", Errors = ModelState });
            }

            var result = await _chatRoomService.RemoveUserFromChatRoomAsync(
                removeUserDto.ChatRoomId,
                removeUserDto.UserId,
                removeUserDto.RequesterId);

            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Failed to remove user from chat room", Errors = result.Errors });
            }

            return Ok(new { Message = "User removed from chat room successfully" });
        }

        // Get all chat rooms for a user
        [HttpGet("UserChatRooms/{userId}")]
        public async Task<IActionResult> GetUserChatRooms(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new { Message = "User ID cannot be null or empty" });
            }

            var chatRooms = await _chatRoomService.GetUserChatRoomsAsync(userId);
            var chatRoomDtos = _mapper.Map<List<ChatRoomDto>>(chatRooms);

            return Ok(new { Message = "User chat rooms retrieved successfully", ChatRooms = chatRoomDtos });
        }
    }
}
