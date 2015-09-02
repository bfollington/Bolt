using SimpleJson;
using System.Collections.Generic;
using UnityEngine;

public class JSON {
	
	public static IDictionary<string, object> Parse(string json) {
		return (IDictionary<string, object>)SimpleJson.SimpleJson.DeserializeObject( json );
	}
	
	public static JsonObject ToJsonObject(object json) {
		return json as JsonObject;
	}
	
	public static uint GetUint(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return System.Convert.ToUInt32( realJson[key] );
	}
	
	public static int GetInt(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return System.Convert.ToInt32( realJson[key] );
	}

	public static float GetFloat(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return System.Convert.ToSingle( realJson[key] );
	}
	
	public static bool GetBool(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return System.Convert.ToBoolean( realJson[key] );
	}

	public static Vector2 GetVector2(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		JsonObject inner = ToJsonObject(realJson[key]);
		var x = System.Convert.ToSingle(inner["x"]);
		var y = System.Convert.ToSingle(inner["y"]);

		return new Vector2(x, y);
	}
	
	public static string GetString(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return System.Convert.ToString( realJson[key] );
	}
	
	public static JsonObject GetObject(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return (realJson[key] as JsonObject);
	}
	
	public static JsonArray GetArray(object json, string key) {
		JsonObject realJson = ToJsonObject(json);
		return (realJson[key] as JsonArray);
	}
	
	
	
}
