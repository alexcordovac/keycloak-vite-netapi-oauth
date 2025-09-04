<template>
  <v-dialog v-model="isOpen" max-width="520">

    <template #activator="{ props }">
      <v-btn v-bind="props" class="mx-2" dark color="green" :disabled="!userStore.scopes.includes('forecast:create')">
        <v-icon size="25">mdi-plus</v-icon>Add
      </v-btn>
    </template>

    <v-card>
      <v-card-title class="text-h6">Create Weather Forecast</v-card-title>
      <v-card-text>
        <v-form ref="formRef" v-model="isValid" @submit.prevent>
          <div class="d-flex flex-column ga-3">
            <v-text-field label="Temperature (°C)" type="number" step="1" v-model="temperatureC"
              :rules="[rules.required]" placeholder="e.g. 22" hide-details="auto" />
            <v-text-field label="Summary (optional)" v-model="summary" placeholder="Sunny, Cloudy, Rainy..."
              hide-details="auto" />
            <v-text-field label="Temperature (°F)" type="number" readonly v-model="temperatureF" variant="outlined"
              hint="Calculated from °C" persistent-hint />
          </div>
        </v-form>
      </v-card-text>

      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" @click="close">Cancel</v-btn>
        <v-btn color="primary" :disabled="!canSubmit" @click="submit">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { useUserStore } from '@/stores/userStore';
import { ref, computed } from 'vue'
import { useMessagesStore } from '@/stores/messageStore'
import api from '@/http/api'

/**
 * Local state
 */
const isOpen = defineModel('isOpen')
const emit = defineEmits(['close'])
const formRef = ref(null)
const isValid = ref(false)
const temperatureC = ref(null)
const summary = ref('')
const userStore = useUserStore()
const messages = useMessagesStore()

/**
 * Validation rules
 */
const rules = {
  required: v => !!v || 'Required'
}

/**
 * Auto-calc Fahrenheit (rounded like the classic example)
 * F = 32 + C * 9/5
 */
const temperatureF = computed(() => {
  const c = Number(temperatureC.value)
  if (Number.isFinite(c)) return Math.round(32 + c * 9 / 5)
  return ''
})

const canSubmit = computed(() => isValid.value && temperatureC.value !== null)

/**
 * Emit/save handler
 * Build payload matching your .NET model casing:
 * { TemperatureC: number, Summary: string|null, TemperatureF: number }
 */
const submit = async () => {
  const ok = await formRef.value?.validate()
  if (!ok?.valid) return

  const payload = {
    TemperatureC: Number(temperatureC.value),
    Summary: summary.value?.trim() || null,
    TemperatureF: Number(temperatureF.value),
  }

  try {
    const res = await api.post("/weather", payload);
    messages.add({ text: "Weather forecast created successfully", color: 'success' })
    close()
  } catch (error) {
    console.error("Error creating weather forecast:", error);
    messages.add({ text: "Error creating weather forecast", color: 'error' })
  }
}

const close = () => {
  reset()
  isOpen.value = false

  emit('close')
}

function reset() {
  formRef.value?.reset()
  formRef.value?.resetValidation()
  temperatureC.value = null
  summary.value = ''
}
</script>
