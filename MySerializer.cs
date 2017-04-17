using System;
using System.Reflection;

namespace Serializer {
	public class MySerializer {
		public static string Serialize(Object obj) {
			Type type = obj.GetType();
			string serial = "";
			foreach (FieldInfo info in type.GetFields()){
				serial += (info.Name + " = " + info.GetValue(obj) +"; ");
			}
			//Console.WriteLine(serial);
			return serial;
		}
		public static T Deserialize<T>(string str) {
			IList<string> values  = str.Split(';').ToList<string>();
			

			Type type = typeof(T);
			ConstructorInfo ctor = type.GetConstructor(new Type[] {});
			T obj = (T)ctor.Invoke(new Object[] {});
			return obj;
		}
	}
	public class Point {
		public int x, y;
		public bool tf;
		public Point() {
			tf = false;
			x = y = 0;
		}
		public Point(int X, int Y, bool tf) {
			x = X;
			y = Y;
			tf = false;
		}
		public void toString(){
			Console.WriteLine("output: {0} {1} {2}", x,y,tf);	
		}
	}
	public class Test {
		public static void Main(String [] args) {	
			Point p1 = new Point(2, 3, true);
			String str1 = MySerializer.Serialize(p1);
			Console.WriteLine(str1);
			Point newPt = MySerializer.Deserialize<Point>(str1);
			newPt.toString();
			Console.WriteLine(newPt);
		}
	}
}

