namespace Sorting
{
    public static class InsertionSort
    {
        public static void InsertionSortMethod(int[] input)
        {

            for (int i = 0; i < input.Count(); i++)
            {
                var item = input[i];
                var currentIndex = i;

                while (currentIndex > 0 && input[currentIndex - 1] > item)
                {
                    input[currentIndex] = input[currentIndex - 1];
                    currentIndex--;
                }

                input[currentIndex] = item;
            }
        }
    }
}