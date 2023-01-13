﻿using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.Service.Messages.Response
{
    public class CreateServiceResponse : BaseResponse
    {
        public ServiceDto Service { get; set; }
    }
}
