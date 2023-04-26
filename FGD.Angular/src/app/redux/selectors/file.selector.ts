import { createFeatureSelector } from '@ngrx/store';
import { Files } from '../transfer-models/file.model.transfer';

export const selectFiles = createFeatureSelector<Files>('file');