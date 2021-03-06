#region License
// =================================================================================================
// Copyright 2018 DataArt, Inc.
// -------------------------------------------------------------------------------------------------
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this work except in compliance with the License.
// You may obtain a copy of the License in the LICENSE file, or at:
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// =================================================================================================
#endregion
using System;
using DataArt.Atlas.CallContext.Correlation;
using DataArt.Atlas.Common.Settings;
using DataArt.Atlas.ServiceDiscovery;
using Flurl;
using Flurl.Http;

namespace DataArt.Atlas.Common
{
    public sealed class ServiceDiscovery : IServiceDiscovery
    {
        private const string Path = "api/v1/discovery";
        private readonly string endpoint;

        public ServiceDiscovery(AtlasSettings settings)
        {
            endpoint = settings.Discovery.DiscoveryEndpoint;
        }

        public Uri ResolveServiceUrl(string serviceKey)
        {
            var url = endpoint
                .AppendPathSegment(Path)
                .SetQueryParam("serviceKey", serviceKey)
                .WithHeader(CorrelationContext.CorrelationIdName, CorrelationContext.CorrelationId)
                .GetJsonAsync<Uri>().Result;

            return url;
        }
    }
}
