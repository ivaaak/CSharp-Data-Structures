namespace Sorting
{
    public class SharedMethods
    {
        // Quick methods which are shared between sorting algos
        public static void exchange(int[] data, int m, int n)
        {
            int temporary;

            temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
