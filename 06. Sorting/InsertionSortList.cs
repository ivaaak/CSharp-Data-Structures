namespace Sorting
{
    public static class InsertionSortList
    {
        public static List<int> InsertionSortList(this List<int> input)
        {
            var clonedList = new List<int>(input.Count);

            for (int i = 0; i < input.Count; i++)
            {
                var item = input[i];
                var currentIndex = i;

                while (currentIndex > 0 && clonedList[currentIndex - 1] > item)
                {
                    currentIndex--;
                }

                clonedList.Insert(currentIndex, item);
            }

            return clonedList;
        }
    }
}