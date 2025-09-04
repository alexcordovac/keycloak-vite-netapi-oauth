import { createApp } from 'vue'
import './style.css'
import router from "./router";
import App from './App.vue'
import { initializeKeycloak } from './keycloak';
import { createPinia } from 'pinia';

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'

const vuetify = createVuetify({
  icons: {
    defaultSet: 'mdi', // This is already the default value - only for display purposes
  },
  components,
  directives
});

//pinia store
const pinia = createPinia()

const app = createApp(App)
app.use(pinia)

initializeKeycloak().then(() => {

  app.use(router)
     .use(vuetify)
     .mount("#app");
});