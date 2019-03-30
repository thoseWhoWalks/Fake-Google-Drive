import { FileModel } from '../module-storage/shared/models/file.model';
import { FolderModel } from '../module-storage/shared/models/folder.model';
import { INavigationStack } from './reducers/navigator.reducer';
import { NotificationModel } from '../module-storage/shared/models/notification.model';
import SubscriptionInfoModel from '../shared/model/subscription/subscription-info.model';

export interface FileState {
    filePage: {
        files: FileModel[],
        selectedFiles: FileModel[]
    }
}

export interface FolderState {
    folderPage: {
        folders: FolderModel[],
        selectedFolders: FolderModel[],
        isInited: boolean
    }
}

export interface NavigatorState {
    stack: INavigationStack
}

export interface AuthState {
    authPage: {
        userId: number,
        token: string
    }
}

export interface SubscriptionState {
    subscription: SubscriptionInfoModel
}

export interface NotificationsState {
    notificationPage: {
        notifications: NotificationModel[]
    }
}