#region copyright
// Copyright (c) CBC/Radio-Canada. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
#endregion

using System;

namespace LinkIt.Core.Interfaces
{
    //Responsible to known the LinkedSourceModelType without using reflection
    public interface ILinkedSourceConfig
    {
        Type LinkedSourceType { get; }
        Type LinkedSourceModelType { get; }
    }
}