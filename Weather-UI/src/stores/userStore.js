import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('user', () => {
  const scopes = ref(null)
  const roles = ref(null)
  const name = ref(null)
  const username = ref(null)


  function setUserScopes(newScopes) {

    if (typeof newScopes === 'string') {
      scopes.value = newScopes.split(' ')

      return;
    }

    scopes.value = newScopes
  }

  function setName(newName) {
    name.value = newName
  }

  function setUsername(newUsername) {
    username.value = newUsername
  }

  function setRoles(newRoles) {
    roles.value = newRoles
  }

  function hasRole(role) {
    return roles.value && roles.value.includes(role)
  }

  return { scopes, setName, name, setUserScopes, username, setUsername, roles, setRoles, hasRole }
})