﻿namespace AssetCove.Contracts.Http;

public class ErrorResponse
{
    public IEnumerable<string> Errors { get; init; }
}