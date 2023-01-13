﻿using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.Service.Messages.Response
{
    public class GetByIdServiceResponse : BaseResponse
    {
        public ServiceDto Service { get; set; }
        public int AccessCount { get; set; }
    }
}
