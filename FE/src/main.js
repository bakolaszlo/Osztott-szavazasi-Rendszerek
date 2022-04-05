import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "@formkit/themes/genesis";
import { plugin, defaultConfig } from "@formkit/vue";

createApp(App).use(plugin, defaultConfig).use(store).use(router).mount("#app");
