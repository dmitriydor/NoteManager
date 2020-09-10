export interface AuthResponse {
  isAuthenticated: boolean;
  accessToken: string;
  errors: string[];
}
