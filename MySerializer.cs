using System;
using System.Reflection;
using System.Collections.Generic;

namespace Serializer {
	public class MySerializer {
		public static string Serialize(Object obj) {
			Type type = obj.GetType();
			string serial = "";
			foreach (FieldInfo info in type.GetFields()){
				serial += (info.Name + ";" + info.GetValue(obj) +";" + info.FieldType.ToString() + ";");
			}
			return serial;
		}
		public static T Deserialize<T>(string str) {
			string[] split = str.Split(';');
            string[] serial = new string[split.Length-1];
         	Array.Copy(split, serial, serial.Length);
			
           	Type type = typeof(T);
           	ConstructorInfo ctor = type.GetConstructor(new Type[] { });
            T obj = (T)ctor.Invoke(new Object[] { });

        	for (int i = 0; i < serial.Length; i ++){
				string name = serial[i];
                string val = serial[i + 1];
            	string valType = serial[i + 2];
                if (valType == "System.Boolean"){
            			obj.GetType().GetField(name).SetValue(obj, bool.Parse(val));
				}
        		if (valType == "System.Double"){
              			obj.GetType().GetField(name).SetValue(obj, double.Parse(val));
				}
           		if (valType == "System.Int32"){
               			obj.GetType().GetField(name).SetValue(obj, int.Parse(val));
				}
				//increment by 3
				i+=2;

            }

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
			Point newPt = MySerializer.Deserialize<Point>(str1);
			Console.WriteLine(newPt.x);
			
			newPt.toString();
			Console.WriteLine(newPt);
		}
	}
}
