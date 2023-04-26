import { createFeatureSelector } from '@ngrx/store';
import { AuthModel } from '../transfer-models/auth.model.transfer';

export const selectAuth = createFeatureSelector<AuthModel>('auth');