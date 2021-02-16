namespace MyRecorder
{
    interface Shadow<T>
    {
        #region step
        int Step { get; set; }

        #endregion
        #region functions
        bool EqualsTo(T other);
        bool EqualsTo(T other, float[] thresholds);
        #endregion
    }
}