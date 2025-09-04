import Keycloak from "keycloak-js";
import { useUserStore } from '@/stores/userStore';

const keycloak = new Keycloak({
    url: "http://localhost:8080/",
    realm: "weather",
    clientId: "weather-ui",
});

export const initializeKeycloak = () =>
    new Promise((resolve, reject) => {
        keycloak
            .init({ onLoad: "login-required", checkLoginIframe: false })
            .then(authenticated => {
                if (!authenticated) {
                    keycloak.login();
                }

                console.log("Keycloak initialized:", keycloak.tokenParsed);
                const userStore = useUserStore();
                userStore.setUserScopes(keycloak.tokenParsed?.scope);
                userStore.setUsername(keycloak.tokenParsed?.preferred_username);
                userStore.setName(keycloak.tokenParsed?.name);
                userStore.setRoles(keycloak.tokenParsed?.realm_access?.roles);

                resolve(keycloak);
            })
            .catch(reject);
    });

export default keycloak;
