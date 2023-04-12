using BazaarOnline.Application.ViewModels.Advertisements;
using BazaarOnline.Application.ViewModels.Conversations;
using Microsoft.AspNetCore.Mvc;

namespace BazaarOnline.API.Controllers
{
    [Route("api/v1/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public TestController()
        {
        }

        [HttpGet("MessagesList")]
        public IActionResult MessagesList()
        {
            var res = new List<MessageDetailViewModel>
            {
                new MessageDetailViewModel
                {
                    Id = Guid.NewGuid(),
                    Data = new MessageDetailDataViewModel
                    {
                        Text = "دو تومن بده بخریم",
                        IsSentBySelf = true,
                        CreateDate = DateTime.Now - TimeSpan.FromMinutes(87),
                        IsSeen = false,
                    }
                },
                new MessageDetailViewModel
                {
                    Id = Guid.NewGuid(),
                    Data = new MessageDetailDataViewModel
                    {
                        Text = "سلام درپو",
                        IsSentBySelf = true,
                        CreateDate = DateTime.Now - TimeSpan.FromMinutes(89),
                        IsSeen = true,
                    }
                },
                new MessageDetailViewModel
                {
                    Id = Guid.NewGuid(),

                    Data = new MessageDetailDataViewModel
                    {
                        Text = "سلام داس",
                        IsSentBySelf = false,
                        CreateDate = DateTime.Now - TimeSpan.FromHours(27),
                    }
                }
            };

            return Ok(res);
        }


        [HttpGet("ConversationList")]
        public IActionResult ConversationList()
        {
            var res = new List<ConversationDetailViewModel>
            {
                new ConversationDetailViewModel
                {
                    Id = Guid.NewGuid(),

                    Data = new ConversationDetailDataViewModel
                    {
                        User = new ConversationDetailDataUserViewModel
                        {
                            Id = "a90510fc-26dd-4922-917b-ed4321a61510",
                            DisplayName = "داس محتین",
                        },
                        Advertisement = new ConversationDetailDataAdvertisementViewModel
                        {
                            Id = 12,
                            Title = "فروش پدران برقی",
                            Picture = new AdvertisementPictureViewModel { FileName = "TesteZajeNzn" }
                        },
                        LastMessage = new MessageDetailViewModel
                        {
                            Id = Guid.NewGuid(),

                            Data = new MessageDetailDataViewModel
                            {
                                Text = "دو تومن بده بخریم",
                                IsSentBySelf = true,
                                CreateDate = DateTime.Now - TimeSpan.FromMinutes(87),
                                IsSeen = false,
                            }
                        },
                    }
                }
            };

            return Ok(res);
        }
    }
}