using System.Reflection;

namespace Habit.Application;

public static class HabitApplicationRoot
{
    public static Assembly Assembly => typeof(HabitApplicationRoot).Assembly;
}
