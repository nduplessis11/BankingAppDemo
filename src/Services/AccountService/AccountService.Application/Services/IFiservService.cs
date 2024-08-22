namespace AccountService.Application.Services;

// Application layer should not have any dependencies on infrastructure layer
// This interface should be implemented in the infrastructure layer
public interface IFiservService
{
    Task<FiservAccountResponse> AddAccountAsync(FiservAccountRequest request);
}

public readonly record struct FiservAccountRequest(
    PartyAcctRelInfo PartyAcctRelInfo,
    DepositAcctInfo DepositAcctInfo,
    OVRDEXCEPTIONDATA OvrExceptionData);

public readonly record struct PartyAcctRelInfo(
    PartyRef PartyRef,
    PartyAcctRelData[] PartyAcctRelData,
    bool TaxReportingOwnerInd,
    bool TaxReportingSignerInd);

public readonly record struct PartyRef(
    PartyKeys PartyKeys);

public readonly record struct PartyKeys(
    string PartyIdentType,
    string PartyIdent);

public readonly record struct PartyAcctRelData(
    string PartyAcctRelType);

public readonly record struct DepositAcctInfo(
    string AcctType,
    string ProductIdent,
    string OpenDt,
    string MaturityDt,
    string Nickname,
    string HandlingCode,
    string SourceOfFunds);

public readonly record struct OVRDEXCEPTIONDATA(
    OVRDEXCEPTION[] OVERRIDEEXCEPTION);

public readonly record struct OVRDEXCEPTION(
    string SUBJECTROLE);

public readonly record struct FiservAccountResponse(
    Status Status,
    AcctStatusRec AcctStatusRec);

public readonly record struct Status(
    string StatusCode,
    string StatusDesc,
    string Severity,
    string SvcProviderName);

public readonly record struct AcctStatusRec(
    AcctKeys AcctKeys,
    AcctStatus AcctStatus);

public readonly record struct AcctKeys(
    string AcctId,
    string AcctType);

public readonly record struct AcctStatus(
    string AcctStatusCode,
    string EffDt);
