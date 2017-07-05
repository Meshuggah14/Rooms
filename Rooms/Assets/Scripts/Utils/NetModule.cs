using System.Collections;
using System.IO;
using System.Xml.Serialization;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
	public partial class NetModule : MonoBehaviour
	{
		private readonly ICoroutineExecutor _executor;
		private Coroutine _currentWorker;

		public NetModule(ICoroutineExecutor executor)
		{
			_executor = executor;
		}

		public void LoadDoc<T>(NetModule.BasewwwRequest<T> XMLRequest) where T : BaseData
		{
			_currentWorker = _executor.StartCoroutine(RequestXML(XMLRequest));
		}

		public void LoadRawImg(BasewwwRequest<Icon> loadPlayerIconRequest)
		{
			_currentWorker = _executor.StartCoroutine(RequestRawImage(loadPlayerIconRequest));
		}

		private IEnumerator RequestXML<T>(NetModule.BasewwwRequest<T> request) where T : BaseData
		{
			string url = request.Url;
			WWW www = new WWW(url);

			yield return www;

			if (www.error != null)
			{
				request.OnError(www.error, request.Url);
				yield break;
			}

			request.OnComplete(ParaseData<T>(www.text));
		}

		private IEnumerator RequestRawImage<T>(NetModule.BasewwwRequest<T> request) where T : BaseData
		{
			string url = request.Url;
			WWW www = new WWW(url);

			yield return www;

			if (www.error != null)
			{
				request.OnError(www.error, request.Url);
				yield break;
			}

			Icon icon = new Icon();
			Texture2D texture = new Texture2D(1, 1);
			texture.LoadImage(www.bytes);
			texture.Apply();
			icon.Value = texture;
			
			request.OnComplete(icon as T);
		}

		private T ParaseData<T>(string text) where T : BaseData
		{
			var serializer = new XmlSerializer(typeof(T));
			return serializer.Deserialize(new StringReader(text)) as T;
		}
	}
}