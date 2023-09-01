namespace SoapExtensions
{
    /// <summary>
    /// Extension methods for Task`1
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Adds a timeout to the task
        /// </summary>
        /// <typeparam name="T">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="timeout">The timeout duration</param>
        /// <returns>The task result or throws if timeout exceeded</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            var delay = Task.Delay(timeout);
            var result = await Task.WhenAny(task, delay);
            if (result == delay)
                throw new TimeoutException();

            return await task;
        }

        /// <summary>
        /// Chains a function after the task completes
        /// </summary>
        /// <typeparam name="T">The task input type</typeparam>
        /// <typeparam name="TV">The task output type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="then">The function to execute after</param>
        /// <returns>A task with the chained result</returns>
        public static async Task<TV> Then<T, TV>(this Task<T> task, Func<T, TV> then)
        {
            var result = await task;
            return then(result);
        }

        /// <summary>
        /// Chains an action after the task completes
        /// </summary>
        /// <typeparam name="T">The task result type</typeparam>
        /// <param name="task">The task</param>
        /// <param name="then">The action to execute after</param>
        /// <returns>A completed task</returns>
        public static async Task Then<T>(this Task<T> task, Action<T> then)
        {
            var result = await task;
            then(result);
        }
    }
}