﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ATTM2X
{
	/// <summary>
	/// Wrapper for AT&T M2X Triggers API
	/// https://m2x.att.com/developer/documentation/v2/device
	/// https://m2x.att.com/developer/documentation/v2/distribution
	/// </summary>
	public sealed class M2XTrigger : M2XClass
	{
		public const string UrlPath = "/triggers";

		public readonly string TriggerId;
		public readonly M2XDevice Device;
		public readonly M2XDistribution Distribution;

		private M2XTrigger(M2XClient client, string triggerId)
			: base(client)
		{
			if (String.IsNullOrWhiteSpace(triggerId))
				throw new ArgumentException(String.Format("Invalid triggerId - {0}", triggerId));

			this.TriggerId = triggerId;
		}
		internal M2XTrigger(M2XDevice device, string streamName)
			: this(device.Client, streamName)
		{
			this.Device = device;
		}
		internal M2XTrigger(M2XDistribution distribution, string streamName)
			: this(distribution.Client, streamName)
		{
			this.Distribution = distribution;
		}

		internal override string BuildPath(string path)
		{
			path = String.Concat(M2XTrigger.UrlPath, "/", WebUtility.UrlEncode(this.TriggerId), path);
			return this.Device == null
				? this.Distribution.BuildPath(path)
				: this.Device.BuildPath(path);
		}

		/// <summary>
		/// Test the specified trigger by firing it with a fake value.
		/// 
		/// This method can be used by developers of client applications
		/// to test the way their apps receive and handle M2X notifications.
		///
		/// https://m2x.att.com/developer/documentation/v2/device#Test-Trigger
		/// https://m2x.att.com/developer/documentation/v2/distribution#Test-Trigger
		/// </summary>
		public Task<M2XResponse> Test(object parms)
		{
			return MakeRequest("/test", M2XClientMethod.POST, parms);
		}
		/// <summary>
		/// Test the specified trigger by firing it with a fake value.
		/// 
		/// This method can be used by developers of client applications
		/// to test the way their apps receive and handle M2X notifications.
		///
		/// https://m2x.att.com/developer/documentation/v2/device#Test-Trigger
		/// https://m2x.att.com/developer/documentation/v2/distribution#Test-Trigger
		/// </summary>
		public Task<M2XResponse> Test(CancellationToken cancellationToken, object parms)
		{
			return MakeRequest(cancellationToken, "/test", M2XClientMethod.POST, parms);
		}
	}
}
