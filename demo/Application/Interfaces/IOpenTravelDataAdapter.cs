using Application.Models.OpenTravelData;

namespace Application.Interfaces;

public interface IOpenTravelDataAdapter<TRecord, TQuery>
    where TRecord : BaseOPTDRecord
    where TQuery : BaseOPTDQuery
{
    Task<TRecord?> Fetch(TQuery request);
}