﻿namespace SilentMike.Application.Common
{
    using System;

    public interface ICurrentRequestService
    {
        Guid CurrentUserId { get; }
    }
}
