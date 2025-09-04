<template>
  <v-dialog v-model="open" max-width="400">
    <template #activator="{ props }">
      
      <v-btn v-bind="props" color="red" variant="elevated" class="mx-2 opacity-100" fab dark icon
        :disabled="!userStore.scopes.includes('forecast:delete')">
        <v-icon size="25">mdi-delete-forever</v-icon>
      </v-btn>
    </template>

    <v-card class="pa-4 text-center">
      <!-- Big warning icon -->
      <v-icon size="64" color="red" class="mb-4">mdi-alert-circle-outline</v-icon>

      <!-- Confirmation message -->
      <div class="text-h6 mb-4">Are you sure you want to delete this forecast?</div>

      <!-- Actions -->
      <v-card-actions class="justify-center">
        <v-btn variant="text" color="grey" @click="close">Cancel</v-btn>
        <v-btn color="red darken-1" dark @click="submit">Accept</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { useUserStore } from '@/stores/userStore'
import { useMessagesStore } from '@/stores/messageStore'
import api from '@/http/api'

const userStore = useUserStore()
const messages = useMessagesStore()
const open = defineModel('isOpen')
const emit = defineEmits(['itemDeleted'])
const props = defineProps(['id'])

const submit = async () => {
  try {
    const res = await api.delete(`/weather/${props.id}`);
    messages.add({ text: "Weather forecast deleted successfully", color: 'success' })

    emit('itemDeleted')
    close()
  } catch (error) {
    console.error("Error deleting weather forecast:", error);
    messages.add({ text: "Error deleting weather forecast", color: 'error' })
  }
}

const close = () => {
  open.value = false
}
</script>
