import './index.css';
import App from './App';
import { AuthContextProvider } from "./contexts/auth-context";
import { BrowserRouter } from "react-router-dom";
import { createRoot } from 'react-dom/client';

createRoot(document.getElementById('root')).render(
<BrowserRouter>
    <AuthContextProvider>
      <App />
    </AuthContextProvider>
  </BrowserRouter>
);

