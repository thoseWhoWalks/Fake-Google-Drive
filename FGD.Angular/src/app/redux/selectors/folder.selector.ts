import { createFeatureSelector } from '@ngrx/store';
import { Folders } from '../transfer-models/folder.model.transfer';

export const selectFolders = createFeatureSelector<Folders>('folder');