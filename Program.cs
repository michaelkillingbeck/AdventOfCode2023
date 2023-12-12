Type dayType = typeof(Day);
IEnumerable<Type> foundDays = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => dayType.IsAssignableFrom(p) &&
            p.IsAbstract == false);

List<Day> days = [];

foreach(Type foundDay in foundDays)
{
    if (Activator.CreateInstance(foundDay) is Day day)
    {
        days.Add(day);
    }
}

days.OrderBy(day => day.DayID)
    .ToList()
    .ForEach(day => day.Run());