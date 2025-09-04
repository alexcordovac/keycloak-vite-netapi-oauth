<template>
  <router-view />

  <v-snackbar-queue v-model="messages.queue"></v-snackbar-queue>
</template>

<script setup>
import { useMessagesStore } from '@/stores/messageStore';
const messages = useMessagesStore()


</script>


<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}
</style>
