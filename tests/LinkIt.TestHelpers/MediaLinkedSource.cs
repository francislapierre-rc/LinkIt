// Copyright (c) CBC/Radio-Canada. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for more information.

using LinkIt.PublicApi;

namespace LinkIt.TestHelpers
{
    public class MediaLinkedSource : ILinkedSource<Media> {
        public Media Model { get; set; }
        public Image SummaryImage { get; set; }
    }
}