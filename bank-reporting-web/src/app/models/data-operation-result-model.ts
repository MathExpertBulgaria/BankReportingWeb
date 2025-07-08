
export enum OperationMessageType {
    Info = 0,
    Error = 1
}

export enum OperationStatus {
    Success = 1,
    InvalidModel = 2,
    NotFound = 3,
    NotFoundInfo = 31,
    ConflictingEntity = 4,
    LockedEntity = 5,
    NotActive = 6,
    NotActiveInfo = 61,
    WriteError = 7,
    GenericError = 9,
    Other = 99
}

export interface OperationAlert {
    accent: string;
    message: string;
}

export interface OperationMessage {
    key: string;
    message: string;
    messageType: OperationMessageType;
    messageTypeString: string;
}

export interface OperationMessages {
    hasMessages: boolean;
    messages: OperationMessage[];
}

export class DataOperationResult<T> {

    constructor(
        public operationStatus: OperationStatus,
        public messages: OperationMessages,
        public data: T
    ) { }

    getAlerts(): OperationAlert[] {
        // map it
        return this.messages.messages.map(c => {
            let accent = 'primary';
            switch (c.messageType) {
                case OperationMessageType.Error:
                    accent = 'warn';
                    break;
                case OperationMessageType.Info:
                    accent = 'info';
                    break;
                default:
                    accent = 'primary';
                    break;
            }

            return {
                accent,
                message: c.message,
            };
        });
    }
}

export function createDORfromError<T>(error: any): DataOperationResult<T> {
    return new DataOperationResult<T>(error.error.operationStatus, error.error.messages, error.error.data as T);
}

export function createDORfromObj<T>(obj: any): DataOperationResult<T> {
    return new DataOperationResult<T>(obj.operationStatus, obj.messages, obj.data as T);
}
