using System;
using Data;
using UnityEngine;

namespace Utils
{
	public partial class NetModule
	{
		public class BasewwwRequest<T> where T : BaseData
		{
			private readonly string _url;
			private readonly Action<string, string> _onError;
			private readonly Action<T> _onComplete;

			public BasewwwRequest(Action<T> onComplete, Action<string, string> onError, string url)
			{
				_onComplete = onComplete;
				_onError = onError;
				_url = url;
			}

			public string Url
			{
				get { return _url; }
			}

			public Action<string, string> OnError
			{
				get { return _onError; }
			}

			public Action<T> OnComplete
			{
				get { return _onComplete; }
			}
		}
	}
}