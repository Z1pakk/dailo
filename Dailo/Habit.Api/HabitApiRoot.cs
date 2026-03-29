using System.Reflection;
using Habit.Application;

namespace Habit.Api;

public static class HabitApiRoot
{
    public static Assembly Assembly => typeof(HabitApiRoot).Assembly;
}
