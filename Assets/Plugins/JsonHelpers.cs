using System;
using System.Collections.Generic;
using FullSerializer;

namespace Zi {
	using fsObject = Dictionary<string, fsData>;

	public static class JsonHelpers {
		public static string GetString(fsData obj, string name, string def) {
			string tryResult;
			return TryGetString(obj, name, out tryResult) ? tryResult : def;
		}
		public static string GetString(fsObject obj, string name, string def) {
			string tryResult;
			return TryGetString(obj, name, out tryResult) ? tryResult : def;
		}
		public static long GetLong(fsData obj, string name, long def) {
			long tryResult;
			return TryGetLong(obj, name, out tryResult) ? tryResult : def;
		}
		public static long GetLong(fsObject obj, string name, long def) {
			long tryResult;
			return TryGetLong(obj, name, out tryResult) ? tryResult : def;
		}
		public static double GetDouble(fsData obj, string name, double def) {
			double tryResult;
			return TryGetDouble(obj, name, out tryResult) ? tryResult : def;
		}
		public static double GetDouble(fsObject obj, string name, double def) {
			double tryResult;
			return TryGetDouble(obj, name, out tryResult) ? tryResult : def;
		}

		public static bool TryGetString(fsData obj, string name, out string result) {
			result = null;
			return obj != null && obj.IsDictionary && TryGetString(obj.AsDictionary, name, out result);
		}

		public static bool TryGetString(fsObject obj, string name, out string result) {
			fsData resultFsData;
			if (obj.TryGetValue(name, out resultFsData)) {
				if (resultFsData.IsString) {
					result = resultFsData.AsString;
					return true;
				}
			}

			result = null;
			return false;
		}

		public static bool TryGetLong(fsData obj, string name, out long result) {
			result = 0;
			return obj != null && obj.IsDictionary && TryGetLong(obj.AsDictionary, name, out result);
		}

		public static bool TryGetLong(fsObject obj, string name, out long result) {
			fsData resultFsData;
			if (obj.TryGetValue(name, out resultFsData)) {
				if (resultFsData.IsInt64) {
					result = resultFsData.AsInt64;
					return true;
				}
			}

			result = 0;
			return false;
		}

		public static bool TryGetDouble(fsData obj, string name, out double result) {
			result = 0.0;
			return obj != null && obj.IsDictionary && TryGetDouble(obj.AsDictionary, name, out result);
		}

		public static bool TryGetDouble(fsObject obj, string name, out double result) {
			fsData resultFsData;
			if (obj.TryGetValue(name, out resultFsData)) {
				if (resultFsData.IsDouble) {
					result = resultFsData.AsDouble;
					return true;
				}
			}

			result = 0.0;
			return false;
		}

		public static bool IsTruthy(fsData obj, string name) {
			return obj != null && obj.IsDictionary && IsTruthy(obj.AsDictionary, name);
		}

		public static bool IsTruthy(fsObject obj, string name) {
			fsData data;
			return obj.TryGetValue(name, out data) && data != null && data.IsBool && data.AsBool;
		}

		public static fsData Merge(fsData target, fsData fromData) {
			if (target == null) {
				return fromData;
			}
			
			if (target.IsDictionary && fromData.IsDictionary) {
				var targetDict = target.AsDictionary;
				foreach(var pair in fromData.AsDictionary) {
					targetDict.Add(pair.Key, pair.Value);
				}

				return target;
			}

			return fromData.IsNull ? target : fromData;
		}

		public static fsData Default(fsData target, fsData def) {
			if (target == null) {
				return def;
			}
			
			if (target.IsDictionary && def.IsDictionary) {
				var targetDict = target.AsDictionary;
				foreach(var pair in def.AsDictionary) {
					if (!targetDict.ContainsKey(pair.Key)) {
						targetDict.Add(pair.Key, pair.Value);
					}
				}

				return target;
			}

			return def.IsNull ? target : def;
		}
	}
}
