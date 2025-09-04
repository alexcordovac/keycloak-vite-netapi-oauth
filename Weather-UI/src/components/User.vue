<template>
    <v-card class="d-flex align-center pa-3 mb-10" outlined variant="elevated" color="indigo"
        style="max-width: min(100vw, 400px); width: 100%;">
        <!-- Avatar circle -->
        <v-avatar size="48" color="black" class="me-3">
            <span class="white--text text-h6">{{ initials }}</span>
        </v-avatar>

        <!-- User info -->
        <div class="text-left w-100">
            <div class="d-flex justify-space-between align-center">
                <div class="text-subtitle-1 font-weight-medium">{{ userStore.name }} </div>
                <v-btn @click="logout" dark color="orange">Logout</v-btn>
            </div>

            <div class="text-caption grey--text">{{ userStore.username }}</div>
        </div>
    </v-card>
</template>

<script setup>
import { useUserStore } from '@/stores/userStore';
import { computed } from 'vue';
import keycloak from '@/keycloak';

const userStore = useUserStore();

const initials = computed(() => {
    const words = userStore.name.split(' ')
    const first = words[0]?.[0] ?? ''
    const last = words.length > 1 ? words[words.length - 1][0] : ''
    return (first + last).toUpperCase()
})

const logout = () => keycloak.logout({ redirectUri: window.location.origin });

</script>

<style scoped>
/* Optional: make avatar text centered and bold */
.v-avatar span {
    font-weight: bold;
    font-size: 1.25rem;
}
</style>
