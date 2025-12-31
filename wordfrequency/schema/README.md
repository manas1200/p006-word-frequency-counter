# Strong Central Schema

This directory demonstrates a robust schema-based approach to building microservices with NATS. A strong, centralized schema is one of the most important foundations for building reliable microservices.

## Why Schema Matters

One of the most important things you would want to do when building microservices is have a strong schema that is central to all of your services. This will help you:

- **Avoid runtime errors** because of malformed messages/events.
- **Document your interfaces** quite easily and enforce a strong governance process on changing the interfaces.
- **Implement input validation** of messages so that your handlers **never** receive invalid messages.

# this is just a demonstration of how strong schema looks like. Please bring in your own central schema.

For more information on how to build central schema please see our [docs](https://github.com/connectionloops/cloops.microservices/blob/main/docs/schema.md)
