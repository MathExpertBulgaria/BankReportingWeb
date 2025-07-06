export interface TransactionModel {
    id?: number;
    idMerchant?: number;
    createDate: Date;
    idDirection?: number;
    amount?: number;
    idCcy?: number;
    debtorIban?: string;
    beneficiaryIban?: string;
    status?: string;
    externalId?: string;
    idTransactionFile?: number;
}