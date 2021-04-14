# ExtentionLibs

//D
public static bool IsNull(this string input)
{
    if (input == null)
        return true;
    return string.IsNullOrWhiteSpace(input.Trim());
}

public static bool IsNotNullOrEmpty(this string input) => !string.IsNullOrEmpty(input);

public static void ThrowIfNullEmptyOrWhiteSpace(this string input)
{
    if (input.IsNullEmptyOrWhiteSpace())
        throw new ArgumentNullException(nameof(input));
}

public static IEnumerable<T> DistinctByIdWithoutYield<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
{
    List<T> ts = new List<T>();
    HashSet<TKey> keys = new HashSet<TKey>();
    foreach (T item in source)
    {
        if (keys.Add(selector(item)))
        {
            ts.Add(item);
        }
    }
    return ts;
}
public static IEnumerable<T> DistinctById<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
{
    HashSet<TKey> keys = new HashSet<TKey>();
    foreach (T item in source)
    {
        if (keys.Add(selector(item)))
        {
            yield return item;
        }
    }
} 
