import { createActionGroup, props } from '@ngrx/store';
import { AuthJWTResponseModel } from 'src/app/module-account/shared/models/auth-jwt-response.model';

export const AuthPageActions = createActionGroup({
    source: 'Page',
    events: {
        'LOG_IN': props<AuthJWTResponseModel>(),
        'LOG_OUT': props<any>(),
    },
});
