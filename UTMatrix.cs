using System; using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTMatrix {
	// This iterator iterates over the upper triangular matrix.
	// This is done in a row major fashion, starting with [0][0],
	// and includes all N*N elements of the matrix.
	public class UTMatrixEnumerator : IEnumerator {
		public UTMatrix matrix;
		public int curIndex;
		public UTMatrixEnumerator(UTMatrix matrix) {
			this.matrix = matrix;
			this.curIndex = 0;
			Reset();
		}
		public void Reset() {
			curIndex = 0;	
		}
		public bool MoveNext() {
			curIndex++;
			return curIndex < matrix.getSize();
		}
		object IEnumerator.Current {
			get {
				return Current;
			}
		}
		public int Current {
			get {
				try {
					return matrix.data[curIndex];
				}
				catch (IndexOutOfRangeException) {
					throw new InvalidOperationException();
				}
			}
		}
	}
	public class UTMatrix : IEnumerable {
		// Must use a one dimensional array, having minumum size.
		public int [] data;
		public int N;

		// Construct an NxN Upper Triangular Matrix, initialized to 0
		// Throws an error if N is non-sensical.
		public UTMatrix(int N) {
			if(N < 1){
				Console.WriteLine("Error. You are trying to initialize a matrix with illogical bounds.");
				throw new IndexOutOfRangeException();
			}
			int size = (N*(N+1))/2;
			data = new int[size];
			this.N = N;
		}
		// Returns the size of the matrix
		public int getSize() {
			return (N*(N+1))/2;
		}
		// Returns an upper triangular matrix that is the summation of a & b.
		// Throws an error if a and b are incompatible.
		public static UTMatrix operator + (UTMatrix a, UTMatrix b) {
			if (a.N != b.N){
				Console.WriteLine("You cannot sum two matrices with different r,c values");
				throw new InvalidOperationException();
			}
			
			UTMatrix ret = new UTMatrix(a.N);
			for (int r=0; r<a.N; r++) {
				for (int c=r; c<a.N; c++) {
					int aVal = a.get(r, c);
					int bVal = b.get(r, c);
					int cVal = aVal + bVal;
					ret.set(r,c,cVal);
				}
			}
			return ret;
		}
		// Set the value at index [r][c] to val.
		// Throws an error if [r][c] is an invalid index to alter.
		public void set(int r, int c, int val) {
			if (r > N-1 || r < 0 || c > N-1 || c < 0 || r > c){
				Console.WriteLine("You are trying to access the 2D matrix out of range.");
				Console.WriteLine("r: " + r + " c: "+c + " N: " +N);
				throw new AccessViolationException();
			}
			int loc = r*N + c - (r*(r+1))/2;
			data[loc] = val;
		}
		// Returns the value at index [r][c]
		// Throws an error if [r][c] is an invalid index
		public int get(int r, int c) {
			if (r > N-1 || r < 0 || c > N-1 || c < 0 || r > c){
				Console.WriteLine("You are trying to access the 2D matrix out of range.");
				throw new AccessViolationException();
			}
			int loc = r*N + c - (r*(r+1))/2;
			int val = data[loc];
			return val;
		}
		// Returns the position in the 1D array for [r][c].
		// Throws an error if [r][c] is an invalid index
		public int accessFunc(int r, int c) {
			if (r > N-1 || r < 0 || c > N-1 || c < 0 || r > c){
				Console.WriteLine("This is not a valid r,c combination for the 2D matrix.");
				throw new AccessViolationException();
			}
			return ((r*N)+c-(r*(r+1))/2);
		}
		// Returns an enumerator for an upper triangular matrix
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
		public UTMatrixEnumerator GetEnumerator() {
			return new UTMatrixEnumerator(this);
		}

		public static void Main(String [] args) {
			
			const int N = 5;
			UTMatrix ut1 = new UTMatrix(N);
			UTMatrix ut2 = new UTMatrix(N);
			for (int r=0; r<N; r++) {
				ut1.set(r, r, 1);
				for (int c=r; c<N; c++) {
			//	Console.WriteLine("r: " + r + " c: "+c);
					ut2.set(r, c, 1);
				}
			}
			UTMatrix ut3 = ut1 + ut2;
			
			UTMatrixEnumerator ie = ut3.GetEnumerator();
			while (ie.MoveNext()) {
				Console.Write(ie.Current + " ");
			}
			Console.WriteLine();
			foreach (int v in ut3.data) {
				Console.Write(v + " ");
			}
			Console.WriteLine();
			
		}
	}
}
