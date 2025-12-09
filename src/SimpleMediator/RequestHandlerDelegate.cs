using System.Diagnostics.CodeAnalysis;
using LanguageExt;

namespace SimpleMediator;

/// <summary>
/// Represents the continuation of the pipeline inside a behavior while honouring the Zero Exceptions policy.
/// </summary>
/// <typeparam name="TResponse">Type returned by the terminal handler.</typeparam>
[SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Delegate suffix communicates the delegate nature of the type and is part of the public API.")]
public delegate Task<Either<Error, TResponse>> RequestHandlerDelegate<TResponse>();
