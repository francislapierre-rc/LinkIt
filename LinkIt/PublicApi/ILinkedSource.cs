﻿#region copyright
// Copyright (c) CBC/Radio-Canada. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
#endregion

namespace LinkIt.PublicApi
{
    /// <summary>
    /// Responsible for defining the link targets of a model
    /// </summary>
    public interface ILinkedSource<TModel>
    {
        TModel Model { get; set; }
    }
}