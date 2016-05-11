using UnityEngine;
using System;
using FullSerializer;
using System.Net;

namespace Zi {
	public delegate void EventHandler(bool isSuccess, HttpStatusCode httpStatusCode, string response);

	public static class WebHelpers {

		private static int retryCount = 1;

		private static string asyncData;

		public static event EventHandler httpResCallbackHandler;

		public static event EventHandler asyncHttpResCallbackHandler;

		public static void CreatHttpRequest(string url = "", string apiKey = "", string param = "", string method = "POST"){
			// ignore SSL certificate errors
			System.Net.ServicePointManager.ServerCertificateValidationCallback += (s,ce,ca,p) => true;

			byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(param);
			HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(url);
			req.Method = method;
			req.ContentType = "application/json";
			req.ContentLength = postBytes.Length;
			req.Headers.Add("Api-Key", apiKey);
			req.Timeout = 5000; //5 seconds
			if (method == "POST" && !string.IsNullOrEmpty(param)) {
				//write request data
				System.IO.Stream writer;
				try {
					writer = req.GetRequestStream ();//get stream object of write request data
				} catch (System.Exception e) {
					writer = null;
					Debug.Log ("retryCount=" + retryCount.ToString () + "连接服务器失败! error:" + e);
					RetryRequest (url, HttpStatusCode.ExpectationFailed, param, method, "连接服务器失败! error:" + e);

					if (retryCount > 3) {
						return;
					}
				}

				if (writer != null) {
					writer.Write (postBytes, 0, postBytes.Length);
					writer.Close ();
				}
			}
			
			HttpWebResponse response = null;
			try{
				response = (HttpWebResponse)req.GetResponse();
				if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK){
					//get response stream
					System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
					string responseJson = reader.ReadToEnd();
					httpResCallbackHandler.Invoke (true, response.StatusCode, responseJson);
					return;
				}else{
					System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
					string errorStr = reader.ReadToEnd();
					Debug.Log("retryCount=" + retryCount.ToString() + "fail:status=" + response.StatusCode + " body=" + errorStr);
					RetryRequest(url, response.StatusCode, param, method, "StatusCode:" + response.StatusCode + " error:" + errorStr);
				}
				response.Close();
			}catch (WebException ex){
				response = ex.Response as HttpWebResponse;
				if (response != null){
					Debug.Log("request error:statusCode=" + response.StatusCode.ToString() + ";statucDes=" + response.StatusDescription.ToString());
					System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
					string responseStr = reader.ReadToEnd();

					httpResCallbackHandler.Invoke (false, response.StatusCode, responseStr);

				} else {
					
					httpResCallbackHandler.Invoke (false, HttpStatusCode.RequestTimeout, "http response is null");
				}
			}

		}

		public static void CreatAsyncHttpRequest(string url = "", string apiKey = "", string param = "", string method = "POST"){
			// ignore SSL certificate errors
			System.Net.ServicePointManager.ServerCertificateValidationCallback += (s,ce,ca,p) => true;

			asyncData = param;

			HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create(url);
			req.Method = method;
			req.ContentType = "application/json";

			req.Headers.Add("Api-Key", apiKey);
			req.Timeout = 5000; //5 seconds
			byte[] postBytes;
			if (method == "POST") {
				postBytes = System.Text.Encoding.UTF8.GetBytes(param);
				req.ContentLength = postBytes.Length;
			}
			Debug.Log("enter CreatAsyncHttpRequest:");
			if (method == "POST") {
				req.BeginGetRequestStream (new AsyncCallback (GetRequestStreamCallback), req);
			} else {
				req.BeginGetResponse (new AsyncCallback (AsyHttpRequestComplete), req);
			}
		}

		private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
			
			// End the operation
			System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult);

			// Convert the string into a byte array. 
			byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(asyncData);
			
			// Write to the request stream.
			postStream.Write(byteArray, 0, asyncData.Length);
			postStream.Close();

			// Start the asynchronous operation to get the response
			request.BeginGetResponse(new AsyncCallback(AsyHttpRequestComplete), request);
		}

		private static void AsyHttpRequestComplete(IAsyncResult asyncResult){
			if (asyncResult == null){
				return;
			}
			HttpWebRequest requese = (asyncResult.AsyncState as HttpWebRequest);
			HttpWebResponse response = requese.EndGetResponse(asyncResult) as HttpWebResponse;
			System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
			string responseJson = reader.ReadToEnd();

			if (response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK) {
				asyncHttpResCallbackHandler.Invoke (true, response.StatusCode, responseJson);
			} else {
				asyncHttpResCallbackHandler.Invoke (false, response.StatusCode, responseJson);
			}
		}

		//if fail retryRequest 3 times
		private static void RetryRequest(string url, HttpStatusCode  httpStatusCode, string param, string method, string errorCode){
			retryCount = retryCount + 1;
			if (retryCount > 3) {
				httpResCallbackHandler.Invoke (false, httpStatusCode, errorCode);
			} else {
				WebHelpers.CreatHttpRequest(url, param, method);
			}
		}

	}
}
