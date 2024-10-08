﻿namespace JpegFileUpdater;

/// <summary>
/// 共通拡張メソッド用クラス
/// </summary>
public static class ExtensionMethods
{
    /// <summary>
    /// 渡された複数のパラメーターのうち1つ以上に一致する場合、trueを返す。
    /// </summary>
    public static bool IsIn<T>(this T source, params T[] values)
        => values.Contains(source);

    /// <summary>
    /// 渡されたコレクション内からnullのアイテムを除いたコレクションを返す。
    /// </summary>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> values) where T : class
        => values.Where(v => v != null)!;

    /// <summary>
    /// 渡されたstringがnull/空文字/スペース文字の場合はnullを、それ以外は渡されたstringの値を返す。
    /// </summary>
    public static string? OrNullIfWhiteSpace(this string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;

    /// <summary>
    /// stringをintに変換した値を返す。
    /// </summary>
    /// <param name="defaultValue">[オプション] 変換できなかった場合に返す値（既定値 = 0）。</param>
    public static int ToInt(this string? s, int defaultValue = 0)
        => int.TryParse(s, out int i) ? i : defaultValue;

    /// <summary>
    /// stringをint（null許容型）に変換した値を返す。変換できなかった場合はnullを返す。
    /// </summary>
    public static int? ToNullableInt(this string? s)
        => int.TryParse(s, out int i) ? i : null;
}
