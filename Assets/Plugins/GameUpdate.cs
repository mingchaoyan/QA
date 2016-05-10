using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUpdate {
	class FileInfo {
		public string name;
		public int size;
		public int version;
	}
	readonly List<FileInfo> files = new List<FileInfo>();

	void CheckUpdate() {
		files.Clear();

	}

}
