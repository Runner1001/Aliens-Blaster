using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrabBag<T> where T : class
{
    private T[] things;
    private T[] shuffledTrasnforms;
    private int currentIndex = 0;

    public GrabBag(T[] things)
    {
        this.things = things;
        ShuffleThings();
    }

    public T Grab()
    {
        if(currentIndex >= shuffledTrasnforms.Length)
        {
            ShuffleThings();
            currentIndex = 0;
        }
        return shuffledTrasnforms[currentIndex++];
    }

    private void ShuffleThings()
    {
        T lastThing = (currentIndex > 0 && currentIndex == shuffledTrasnforms.Length) ? shuffledTrasnforms[currentIndex - 1] : null;

        shuffledTrasnforms = things.OrderBy(t=> Random.value).ToArray();

        if(shuffledTrasnforms.Length > 1 && shuffledTrasnforms[0] == lastThing)
        {
            shuffledTrasnforms[0] = shuffledTrasnforms[1];
            shuffledTrasnforms[1] = lastThing;
        }
    }
}