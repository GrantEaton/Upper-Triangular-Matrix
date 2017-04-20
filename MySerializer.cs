using System;
using System.Reflection;
using System.Collections.Generic;

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
			string[] values  = str.Split(';');
			Console.WriteLine(values);
			foreach(string val in values){
				string[] field  = val.Split('=');
				if(field[1].indexOf('.') != -1){
					double v = double.Parse(field[1], System.Globalization.CultureInfo.InvariantCulture);
					Console.WriteLine(v);
					Set(obj,field[0],v);
				}
				else if(isDigit(field[1][0])){
					int v = int32.Parse(field[1]); 
					Console.WriteLine(v);
					Set(obj,field[0],v);	
				}
				

				Console.WriteLine(field[0] + ", " + field[1]);
			}
			
			Type type = typeof(T);
			ConstructorInfo ctor = type.GetConstructor(new Type[] {});
			T obj = (T)ctor.Invoke(new Object[] {});

			
			return obj;
		}
		public static void Set<T>(T o, String fieldName, Object v) {
			Console.WriteLine("Setting: " + o + " " + fieldName  + " with " + v);
			Type t = o.GetType();
			FieldInfo info = t.GetField(fieldName);
			
			info.SetValue(o,v);
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
