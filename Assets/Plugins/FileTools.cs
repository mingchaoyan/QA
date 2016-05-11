using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class FileTools {
	public static bool CreateFileAndDirectory(string filePath, byte[] buffer) {
		bool success = false;
		try {
			string direcory = GetDirectoryNameByPath(filePath);
			if(!Directory.Exists(direcory)) {
				CreateDirectory(direcory);
			}
			success = CreateFile(filePath, buffer);
		} catch (Exception ex){
			Debug.LogError(ex.ToString());
		}
		return success;
	}

	public static void CreateDirectory(string path) {
		if(!Directory.Exists(path)) {
			Directory.CreateDirectory(path);
		}
	}

	public static bool CreateFileForce(string filePath, byte[] buffer) {
		try {
			if(File.Exists(filePath)) {
				File.Delete(filePath);
			}
		} catch (Exception ex) {
			Debug.LogError(ex.ToString());
		}
		return CreateFile(filePath, buffer);
	}

	public static bool CreateFile(string filePath, byte[] buffer) {
		bool success = false;
		try {
			if(!File.Exists(filePath)) {
				FileInfo fi = new FileInfo(filePath);
				FileStream fs = fi.Create();
				fs.Write(buffer,0,buffer.Length);
				fs.Close();
				success = true;
			}
		} catch (Exception ex) {
			Debug.LogError(ex.ToString());
		}
		return success;
	}

	public static string GetDirectoryNameByPath(string filePath) {
		FileInfo fi = new FileInfo(filePath);
		return fi.DirectoryName;
	}
}
