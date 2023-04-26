import { createReducer, on } from "@ngrx/store";
import { AuthPageActions } from "../actions/auth.action";
import { AuthState } from "../app.state";

const initialState = {
    authPage: {
        userId: +localStorage.getItem("userId"),
        token: localStorage.getItem("token")
    }
}

export const authReducer = createReducer(
    initialState,
    on(AuthPageActions.log_in, (state: AuthState, payload) => {

        if (payload.token === null || payload.token === "")
            return state;

        localStorage.setItem("token", payload.token);
        localStorage.setItem("userId", payload.userId);

        return {
            ...state,
            userId: payload.userId,
            token: payload.token
        }
    }),
    on(AuthPageActions.log_out, (state: AuthState) => {

        localStorage.removeItem("token");
        localStorage.removeItem("userId");

        return {
            ...state,
            userId: -1,
            token: "",
        }
    }),
)
