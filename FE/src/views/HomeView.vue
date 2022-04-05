<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
    <FormVue></FormVue>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from "@/components/HelloWorld.vue";
import FormVue from "@/components/FormTemplate.vue";
export default {
  name: "HomeView",
  components: {
    HelloWorld,
    FormVue,
  },
};
</script>
