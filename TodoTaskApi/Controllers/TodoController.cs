using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi.Models;
using TodoTaskApi.Models.Class;
using TodoTaskApi.Models.Dto;

namespace TodoTaskApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        public TodoController(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //This is method for get all Todo Item
        [HttpGet]
        public List<GetTodoDto>GetAllTodo()
        {
            var data = (from a in _context.TodoItem
                        select new GetTodoDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Date = a.Date,
                            isComplete = a.isComplete
                        }).ToList();

            return data;
        }

        //This is method for get all incoming Todo Item 
        [HttpGet]
        public List<GetTodoDto> GetAllIncomingTodo()
        {
            var data = (from a in _context.TodoItem
                        where DateTime.Now < Convert.ToDateTime(a.Date)
                        select new GetTodoDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Date = a.Date,
                            isComplete = a.isComplete
                        }).ToList();

            return data;
        }

        //This is method for get Todo with specific keyword Item
        [HttpGet]
        public List<GetTodoDto> GetSpecificTodo(string keywoard, Boolean? isComplete)
        {
            var data = (from a in _context.TodoItem
                        select new GetTodoDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Date = a.Date,
                            isComplete = a.isComplete
                        }).ToList();

            //code for if user only fill keywoard value
            if (!string.IsNullOrEmpty(keywoard) && isComplete == null)
            {
                data = (from a in _context.TodoItem
                            where a.Name.Contains(keywoard) || a.Date.Contains(keywoard)
                            select new GetTodoDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Date = a.Date,
                                isComplete = a.isComplete
                            }).ToList();
            }

            //code for if user only want to find data with isComplete filter
            if (isComplete != null && string.IsNullOrEmpty(keywoard))
            {
                data = (from a in _context.TodoItem
                            where a.isComplete == Convert.ToInt32(isComplete)
                            select new GetTodoDto
                            {
                                Id = a.Id,
                                Name = a.Name,
                                Date = a.Date,
                                isComplete = a.isComplete
                            }).ToList();
            }

            //code for if user want to find data with isComplete filter and keywoard filter
            if (isComplete != null && !string.IsNullOrEmpty(keywoard))
            {
                data = (from a in _context.TodoItem
                        where a.isComplete == Convert.ToInt32(isComplete) && a.Name.Contains(keywoard) || a.Date.Contains(keywoard)
                        select new GetTodoDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Date = a.Date,
                            isComplete = a.isComplete
                        }).ToList();
            }
            return data;
        }

        //This is method for insert Todo Item
        [HttpPost]
        public GetTodoDto InsertTodo(InsertTodoDto input)
        {
            var data = new TodoItem
            {
                Name = input.Name != null? input.Name : null,
                Date = input.Date != null? Convert.ToString(input.Date.Date):null,
                isComplete = input.isComplete != null? Convert.ToInt32(input.isComplete) : (int?)null
            };

            _context.TodoItem.Add(data);
            _context.SaveChanges();

            var getDataId = data.Id;

            var getData = (from a in _context.TodoItem
                           where a.Id == getDataId
                           select new GetTodoDto
                           {
                               Id = a.Id,
                               Date = a.Date,
                               Name = a.Name,
                               isComplete = a.isComplete
                           }).FirstOrDefault();

            return getData;
        }

        //This is method for update Todo Item
        [HttpPut]
        public GetTodoDto UpdateTodo(UpdateTodoDto input)
        {
            var todo = (from a in _context.TodoItem
                        where a.Id == input.Id
                        select a).FirstOrDefault();

            var data = _mapper.Map<TodoItem>(todo);
            data.Name = input.Name;
            data.isComplete = Convert.ToInt32(input.isComplete);
            data.Date = Convert.ToString(input.Date.Date);

            _context.TodoItem.Update(data);
            _context.SaveChanges();

            var getDataId = data.Id;

            var getData = (from a in _context.TodoItem
                           where a.Id == getDataId
                           select new GetTodoDto
                           {
                               Id = a.Id,
                               Date = a.Date,
                               Name = a.Name,
                               isComplete = a.isComplete
                           }).FirstOrDefault();

            return getData;

        }

        //This is method for update todo isComplete status from false to true, or can be interpreted from inComplate to Complate
        [HttpPut]
        public ResultMessageDto UpdateComplateTodo(int id)
        {
            var todo = (from a in _context.TodoItem
                        where a.Id == id
                        select a).FirstOrDefault();

            if(todo.isComplete == 0)
            {
                var data = _mapper.Map<TodoItem>(todo);
                data.isComplete = Convert.ToInt32(true);
                _context.TodoItem.Update(data);
                _context.SaveChanges();

                return new ResultMessageDto
                {
                    message = "Task successfully updated",
                    result = true
                };
            }
            else
            {
                return new ResultMessageDto
                {
                    message = "Task has been complete",
                    result = true
                };
            }
        }

        //This is method for delete todo
        [HttpDelete]
        public ResultMessageDto DeleteTodo (int id)
        {
            var todo = (from a in _context.TodoItem
                        where a.Id == id
                        select a).FirstOrDefault();

            if(todo != null)
            {
                var data = _mapper.Map<TodoItem>(todo);
                _context.TodoItem.Remove(data);
                _context.SaveChanges();
                return new ResultMessageDto
                {
                    message = "data has been deleted",
                    result = true
                };
            }
            else
            {
                return new ResultMessageDto
                {
                    message = "data not found",
                    result = true
                };
            }
        }

    }
}
