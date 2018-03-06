using System;

public class Class1
{
	public Class1()
	{
	}
}
public class PermutationCalculator
{
    int Fact(int cnt)
    {
        int sum = 1;
        while (cnt > 0)
        {
            sum *= cnt;
            cnt--;
        }
        return sum;
    }
    public List<char[]> Calculate(char[] items)
    {
        if (items == null || items.Length == 0)
            return new List<char[]>();
        int length = items.Length;
        int currentPosition = length - 1;
        int totalItem = Fact(length);
        List<char[]> currentResult = new List<char[]>(totalItem);
        List<char[]> previousResult = new List<char[]>(totalItem);
        //Add last item to the previousResult list
        previousResult.Add(new char[] { items[currentPosition] });
        while (currentPosition > 0)
        {
            currentResult.Clear();
            foreach (var item in previousResult)
            {
                currentResult.AddRange(AddItem(item, items[currentPosition - 1]));
            }
            if (currentPosition - 1 > 0)
            {
                previousResult.Clear();
                previousResult.AddRange(currentResult);
            }
            currentPosition--;
        }
        return currentResult;
    }

    private List<char[]> AddItem(char[] currentItem, char newItem)
    {
        List<char[]> result = new List<char[]>(currentItem.Length + 1);
        int pos = 0;
        while (pos <= currentItem.Length)
        {
            char[] item = new char[currentItem.Length + 1];
            int i = 0, j = 0;

            while (j < currentItem.Length + 1)
            {
                if (j == pos)
                {
                    item[j] = newItem;

                }
                else
                {
                    item[j] = currentItem[i++];
                }

                j++;
            }

            result.Add(item);
            pos++;
        }

        return result;
    }

    public List<char[]> CalculateParallel(char[] items)
    {
        if (items == null || items.Length == 0)
            return new List<char[]>();
        int length = items.Length;
        int currentPosition = length - 1;
        int totalItem = Fact(length);
        var currentResult = new List<char[]>(totalItem);
        var previousResult = new List<char[]>(totalItem);
        int taskLength = 1000;
        previousResult.Add(new char[] { items[currentPosition] });

        while (currentPosition > 0)
        {
            currentResult.Clear();
            int count = previousResult.Count;
            int numTask = count / taskLength;
            if (count % taskLength != 0)
                numTask += 1;

            Task<List<char[]>>[] tasks = new Task<List<char[]>>[numTask];
            int startTask = 0;
            var position = currentPosition;
            int taskInd = 0;
            while (taskInd < numTask)
            {
                tasks[taskInd++] = Task.Factory.StartNew(e =>
                {
                    int start = (int)e;
                    int end = start + taskLength;
                    if (end > previousResult.Count)
                        end = previousResult.Count;
                    var mylist = new List<char[]>((end - start) * (previousResult[0].Length + 1));
                    for (int i = start; i < end; i++)
                    {
                        mylist.AddRange(AddItem(previousResult[i], items[position - 1]));
                    }
                    return mylist;
                }, startTask);
                startTask += taskLength;
            }
            Task.WaitAll(tasks);
            foreach (var task in tasks)
            {
                currentResult.AddRange(task.Result);
                task.Dispose();
            }

            if (currentPosition - 1 > 0)
            {
                previousResult.Clear();
                previousResult.AddRange(currentResult);
            }

            currentPosition--;
        }

        return currentResult;
    }
}